// tslint:disable:curly
import { Component, OnInit, Inject, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { AccountService } from '../../core';
import { ChatClient, ChatAvailableUsersListDto, ChatAvailableUserDto, UserMessageDto, MessageTransferredDto } from '../../../apiDefinitions';
import { MyNotificationsService } from '../../core/services/my-notifications.service';
import { Status } from '../../core/models/status';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatComponent implements OnInit, OnDestroy {


  private _hubConnection: HubConnection;
  private _messageCount = 10;
  private _lastId = 0;

  public searchMask = '';
  public availableUsers: ChatAvailableUsersListDto;
  public currentCullocator: ChatAvailableUserDto;

  message = '';
  messages: UserMessageDto[] = [];

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private cdRef: ChangeDetectorRef,
    private accountService: AccountService,
    private chatClient: ChatClient,
    private router: Router,
    private notificationsService: MyNotificationsService
  ) { }

  ngOnInit() {
    if (!this.accountService.isLoggedIn)
      this.router.navigate(['login']);

    this.hubInit();
  }

  async ngOnDestroy(): Promise<void> {
    await this._hubConnection.stop();
  }

  public sendMessage(): void {
    const data = <UserMessageDto>{
      isMine: true,
      message: this.message,
      time: new Date()
    };

    this.sendMessageViaController(data);
  }

  search() {
    const self = this;

    this.chatClient.getUsersForChat(this.searchMask)
      .subscribe(x => {
        self.availableUsers = x;
        self.cdRef.markForCheck();
      });
  }

  selectUserForConversation(user: ChatAvailableUserDto) {
    this.currentCullocator = user;
    this._lastId = 0;
    const self = this;

    this.chatClient.getPreviousMessages(user.id, this._messageCount, this._lastId)
      .subscribe(messages => {
        self.messages.length = 0;
        self.messages.push(...messages);
        self.cdRef.markForCheck();
      });
  }

  getMoreMessages() {
    this._lastId = this.messages[0].id;
    const self = this;

    this.chatClient.getPreviousMessages(this.currentCullocator.id, this._messageCount, this._lastId)
      .subscribe(messages => {
        if (messages == null || messages.length === 0)
          return;

        self.messages.unshift(...messages);
        self.cdRef.markForCheck();
      });
  }

  private successMessageSend(data: UserMessageDto) {
    this.messages.push(data);
    this.message = '';
    this.cdRef.markForCheck();
  }

  private sendMessageViaController(data: UserMessageDto) {
    const self = this;
    this.chatClient.getSignalRReceiverIdAndSend(data.message, this.currentCullocator.id)
      .subscribe(id => {
        self.currentCullocator.signalRId = id;
        self.successMessageSend(data);
      });
  }

  private hubInit() {
    const accountToken = this.accountService.accessToken;
    const connectionOptions = { accessTokenFactory: () => accountToken } as IHttpConnectionOptions;

    this._hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.baseUrl}chathub`, connectionOptions)
      .build();

    const self = this;
    this._hubConnection.on('send', (messageDto: MessageTransferredDto) => {

      if (this.currentCullocator.id !== messageDto.fromUserId) {
        this.notificationsService.notify(messageDto.message, Status.Info, `Message from user: ${messageDto.fromUserName}`);
        return;
      }

      const received = <UserMessageDto>{
        isMine: false,
        time: new Date(),
        message: messageDto.message
      };
      self.messages.push(received);
      self.cdRef.markForCheck();
    });

    this._hubConnection.start()
      .then(() => {
        console.log('Hub connection started');
      })
      .catch(err => {
        console.log('Error while establishing connection: ' + err);
      });
  }
}
