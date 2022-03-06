import { Status } from './status';

export class MessageDescription {
    constructor(public messageParts: string[], public status: Status, public title?: string) { }
}
