export class Settings {
    // width: number;
    // height: number;

    size: SizeSettings;
    words: WordSettings[];
}

export class SizeSettings {
    width: number;
    height: number;
}

export class WordSettings {
    name: string;
    direction: string;
}