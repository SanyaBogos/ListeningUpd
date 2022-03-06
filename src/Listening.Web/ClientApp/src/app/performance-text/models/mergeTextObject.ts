import { ErrorForSeparatedDto } from '../../../apiDefinitions';

export class MergeTextObject {
    constructor(
        public mergedText: string[][][],
        public resultsEncodedString: boolean[],
        public errorsForJoined: string[] = [],
        public errorsForSeparated: ErrorForSeparatedDto[] = [],
        public type: 'j' | 's' | 'p' = 'j'
    ) {
        mergedText = mergedText;
        resultsEncodedString = resultsEncodedString;
        errorsForSeparated = errorsForSeparated;
        type = type;
    }

}
