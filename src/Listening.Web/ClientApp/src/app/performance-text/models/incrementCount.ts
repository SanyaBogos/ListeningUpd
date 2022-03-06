import { DiagramElementType } from './diagram';

export class IncrementCount {
    constructor(
        public fieldForIncrement: DiagramElementType,
        public count: number
    ) { }
}
