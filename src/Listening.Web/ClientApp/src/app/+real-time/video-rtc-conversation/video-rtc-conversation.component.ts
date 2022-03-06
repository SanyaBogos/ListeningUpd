// /// <reference path="../../../assets/easyrtc/typescript_support/d.ts.files/client/easyrtc.d.ts" />
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy, ViewChild } from '@angular/core';
import { ClientRTC, AckMsg } from '../models/clientRTC';
import { ModalComponent } from '@app/shared/directives/modal/modal.component';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { AccountService } from '@app/core';
import { ajaxSetup } from 'jquery';
import { AccountClient } from 'apiDefinitions';

@Component({
  selector: 'appc-video-rtc-conversation',
  templateUrl: './video-rtc-conversation.component.html',
  styleUrls: ['./video-rtc-conversation.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VideoRtcConversationComponent implements OnInit, OnDestroy {

  @ViewChild('confirmCall', { static: false })
  private readonly confirmCallModal: ModalComponent;

  @ViewChild('offerCall', { static: false })
  private readonly offerCallModal: ModalComponent;

  private callRoom = 'call-room-';
  private defaultGlobalRoom = 'global-room';
  public otherClients: ClientRTC[] = [];
  public isCall: boolean = false;
  public myVideoErtcId: string;
  public otherErtcId: string;
  private availableClientIds: string[] = [];
  private joinedRooms: string[] = [];

  constructor(
    private cdRef: ChangeDetectorRef,
    private notificationsService: MyNotificationsService,
    private accountService: AccountService,
    private accountClient: AccountClient,
  ) { }

  ngOnInit(): void {
    const self = this;
    easyrtc.setVideoDims(1280, 720, 10);
    easyrtc.enableDebug(false);
    easyrtc.setPeerListener((callerId: string, msgType: string, content: string, targeting: { targetRoom: string }) => {

      if (content == 's') {
        this.otherErtcId = callerId;
        this.confirmCallModal.show();
      }
      else if (content == 'a') {
        this.acceptCall();
      }
      else {
        this.localHangUp();
        this.confirmCallModal.hide();
        this.offerCallModal.hide();
      }

      this.cdRef.markForCheck();
    });

    easyrtc.setRoomOccupantListener((roomName: string, list: { [easyrtcId: string]: ClientRTC }, selfInfo: ClientRTC) => {
      console.log(roomName);
      if (roomName != this.defaultGlobalRoom)
        return;

      self.otherClients = [];
      console.log(list);
      
      for (var easyrtcid in list) {
        if (easyrtcid === selfInfo.easyrtcid)
          continue;

        self.otherClients.push(list[easyrtcid]);
      }

      self.cdRef.markForCheck();
    });

    easyrtc.easyApp("easyrtc.videoChatHd", "selfVideo", ["callerVideo"],
      (easyrtcId: string) => {
        this.myVideoErtcId = easyrtcId;

        easyrtc.joinRoom(`${this.defaultGlobalRoom}`, null,
          (roomName: string) => {
            console.log(`call room successfully created with name '${roomName}'`);
          }, (errorCode: string, errorText: string, roomName: string) => {
            console.error(errorCode, errorText + ": room name was(" + roomName + ")");
            this.notificationsService.notify('rtc_general_error', Status.Error)
          });

        easyrtc.joinRoom(`${this.callRoom}${self.myVideoErtcId}`, null,
          (roomName: string) => {
            console.log(`call room successfully created with name '${roomName}'`);
          }, (errorCode: string, errorText: string, roomName: string) => {
            console.error(errorCode, errorText + ": room name was(" + roomName + ")");
            this.notificationsService.notify('rtc_general_error', Status.Error)
          });

        this.cdRef.markForCheck();
      },
      (errorCode: any, message: string) => {
        console.error(errorCode, message)
        this.notificationsService.notify('rtc_general_error', Status.Error)
      },
      self.accountService.accessToken
    );

    // Sets calls so they are automatically accepted (this is default behaviour)
    easyrtc.setAcceptChecker((callerId: string, cb: Function) => {
      if (self.availableClientIds.includes(callerId))
        cb(true);
      else
        cb(false);
    });

    this.cdRef.markForCheck();
  }

  ngOnDestroy() {
    easyrtc.hangupAll();
    easyrtc.disconnect();
    ajaxSetup({ beforeSend: null });
  }

  performCall(otherEasyrtcid: string) {
    if (!this.availableClientIds.includes(otherEasyrtcid))
      this.availableClientIds.push(otherEasyrtcid);

    this.otherErtcId = otherEasyrtcid;
    this.performCallRequest(otherEasyrtcid, 's');
    this.offerCallModal.show();
  }

  acceptCallRequest() {
    this.confirmCallModal.hide();
    this.availableClientIds.push(this.otherErtcId);
    this.performCallRequest(this.otherErtcId, 'a');
    this.isCall = true;
  }

  rejectCallRequest() {
    this.confirmCallModal.hide();
    this.performCallRequest(this.otherErtcId, 'r');
  }

  cancelCallRequest() {
    this.offerCallModal.hide();
    this.performCallRequest(this.otherErtcId, 'r');
  }

  acceptCall() {
    this.confirmCallModal.hide();
    this.offerCallModal.hide();
    easyrtc.hangupAll();

    easyrtc.call(this.otherErtcId, () => { }, () => { }, (accepted: boolean, callerId: string) => {
      if (!accepted) {
        console.error("CALL-REJECTED", "Sorry, your call to " + easyrtc.idToName(callerId) + " was rejected");
        this.notificationsService.notify('rtc_general_error', Status.Error)
      }
      else
        this.isCall = true;

      this.cdRef.markForCheck();
    });
  }

  hangUp() {
    easyrtc.hangup(this.otherErtcId);

    this.performCallRequest(this.otherErtcId, 'r');
    this.localHangUp();
    this.cdRef.markForCheck();
  }

  checkConnect() {
    this.accountClient.whoAmI().subscribe((x) => {
      console.log(x);
    });
  }

  private localHangUp() {
    this.isCall = false;
    this.availableClientIds = this.availableClientIds.filter(x => x != this.otherErtcId);
    this.otherErtcId = null;
  }

  private performCallRequest(otherEasyrtcid: string, marker: 's' | 'a' | 'r') {
    const self = this;
    const calleeRoom = `${this.callRoom}${otherEasyrtcid}`;

    if (!self.joinedRooms.includes(calleeRoom))
      easyrtc.joinRoom(calleeRoom, null,
        (roomName: string) => {
          console.log(`call room successfully created with name '${roomName}'`);
          self.joinedRooms.push(calleeRoom);
          self.sendCallRequest(calleeRoom, marker);
        }, (errorCode: string, errorText: string, roomName: string) => {
          console.error(errorCode, errorText + ": room name was(" + roomName + ")");
          this.notificationsService.notify('rtc_general_error', Status.Error)
        });
    else
      self.sendCallRequest(calleeRoom, marker);
  }

  private sendCallRequest(calleeRoom: string, marker: 's' | 'a' | 'r') {
    easyrtc.sendData({ targetRoom: calleeRoom }, "call-request", marker,
      function (ackMsg: AckMsg) {
        if (ackMsg.msgType === "error") {
          console.error(ackMsg.msgData.errorCode, ackMsg.msgData.errorText);
          this.notificationsService.notify('rtc_general_error', Status.Error)
        }
      }
    );
  }
}
