import { MessageDescription } from './messageDescription';

export class MessageDescriptionEnhanced extends MessageDescription {

  constructor(
    public id: number,
    public description: MessageDescription,
    public timeoutCallbackNumber: number
  ) {
    super(description.messageParts, description.status, description.title);
  }

}
