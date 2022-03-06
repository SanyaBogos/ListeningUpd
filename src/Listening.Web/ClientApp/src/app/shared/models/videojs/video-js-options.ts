export class VideoJsOptions {
    controls: boolean;
    width: number | undefined;
    height: number | undefined;
    fluid: boolean;
    controlBar: ControlBar | null | undefined;
    plugins: Plugins | null | undefined;

    constructor() {
        this.controls = true;
        this.fluid = false;
        this.plugins = new Plugins();
    }
}

export class ControlBar {
    volumePanel: boolean;
    fullscreenToggle: boolean;
}

export class Plugins {
    record: Record;
    wavesurfer: Wavesurfer | null | undefined;
    // videoMimeType: string | null;

    constructor() {
        this.record = new Record();
    }
}

export class Wavesurfer {
    src: string;
    waveColor: string;
    progressColor: string;
    debug: boolean;
    cursorWidth: number;
    msDisplayMax: number;
    hideScrollbar: boolean;
}

export class Record {
    audio: boolean;
    video: boolean;
    screen: boolean;
    maxLength: number | undefined;
    debug: boolean;

    constructor() {

    }
}
