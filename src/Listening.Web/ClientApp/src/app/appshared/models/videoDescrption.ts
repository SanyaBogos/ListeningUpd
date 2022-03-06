export class VideoDescription {
    name: string;
    src: string;
    type: string;
    isVisible?: boolean = false;
    description?: string;
    isAllowed?: boolean;
    track?: string;
}