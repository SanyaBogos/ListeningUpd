export class Presence {
    show: string;
    status: any;
}

export class ClientRTC {
    easyrtcid: string;
    presence: Presence;
    roomJoinTime: number;
    roomName: string;
    roomStatus: string;
}

export class MsgData {
    errorCode: string;
    errorText: string;
}

export class AckMsg {
    msgType: string;
    msgData: MsgData
}

// export class PeerListenerOptions {
//     callerId: string;
//     msgType: string;
//     content: string;
//     targeting: Targeting
// }

// export class Targeting {
//     targetRoom: string
// }

// export class RoomOccupantListenerOptions {

// }