import { Injectable } from '@angular/core';
import { StegSettingsDto } from 'apiDefinitions';
import { DataOperationsService } from './data-operations.service';
// import { Settings } from './models/settings';

@Injectable()
export class ImgTransformService {

  constructor(
    private dataOperationsService: DataOperationsService
  ) { }

  // simple (one-by-one injection from start to red green blue alpha LSB)
  public simpleInjectMessage(imageData: ImageData, message: string, settings: StegSettingsDto): ImageData {

    const bytes = this.dataOperationsService.stringToBytes(message);
    bytes.unshift(message.length);
    const bits = this.dataOperationsService.numbersToBits(bytes);

    const resultImageData = new ImageData(imageData.width, imageData.height);
    const fullColorList = Array.from({ length: 4 }, (v, i) => i)

    let i = 0, ii = 0;
    const startIndexEnh = settings.startIndex * 4;
    const stepEnh = settings.step * 4;

    if (settings.mode == 's') {
      // settings.simple.startIndex
      for (i = 0; i < imageData.data.length; i += 4) {

        let isSet = (i >= startIndexEnh) && (i - startIndexEnh) % stepEnh == 0 && ii < bits.length;

        if (isSet) {
          for (let j = 0; j < settings.colors.length; j++) {
            const index = i + settings.colors[j];

            if (index == 509) {
              let xx = 11;
              console.log(xx);
            }

            console.log(`${index} \t ${bits[ii]}`);

            resultImageData.data[index] = this.setLSB(imageData.data[index], bits[ii++]);
          }

          var last = fullColorList.filter(x => !settings.colors.includes(x));

          for (let j = 0; j < last.length; j++) {
            const index = i + last[j];
            resultImageData.data[index] = imageData.data[index];
          }
        }
        else {
          resultImageData.data[i] = imageData.data[i];
          resultImageData.data[i + 1] = imageData.data[i + 1];
          resultImageData.data[i + 2] = imageData.data[i + 2];
          resultImageData.data[i + 3] = imageData.data[i + 3];
        }

        // resultImageData.data[i] = this.setLSB(imageData.data[i], bits[i]);
        // resultImageData.data[i + 1] = this.setLSB(imageData.data[i + 1], bits[i + 1]);
        // resultImageData.data[i + 2] = this.setLSB(imageData.data[i + 2], bits[i + 2]);
        // resultImageData.data[i + 3] = this.setLSB(imageData.data[i + 3], bits[i + 3]);
      }

    }

    // for (i = 0; i < imageData.data.length && i < bits.length; i += 4) {
    //   resultImageData.data[i] = this.setLSB(imageData.data[i], bits[i]);
    //   resultImageData.data[i + 1] = this.setLSB(imageData.data[i + 1], bits[i + 1]);
    //   resultImageData.data[i + 2] = this.setLSB(imageData.data[i + 2], bits[i + 2]);
    //   resultImageData.data[i + 3] = this.setLSB(imageData.data[i + 3], bits[i + 3]);
    // }

    // for (let j = i; j < imageData.data.length; j += 4) {
    //   resultImageData.data[j] = imageData.data[j];
    //   resultImageData.data[j + 1] = imageData.data[j + 1];
    //   resultImageData.data[j + 2] = imageData.data[j + 2];
    //   resultImageData.data[j + 3] = imageData.data[j + 3];
    // }

    return resultImageData;
  }

  public simpleEjectMessage(imageData: ImageData, settings: StegSettingsDto): string {

    const lengthBits: boolean[] = [];
    const bufferLength = this.dataOperationsService.getBufferLength();
    const { startIndex, step } = settings;

    let i = startIndex * 4;

    console.log(i);

    for (let j = 0; j < bufferLength; i += step * 4) {
      for (let k = 0; k < settings.colors.length; k++, j++) {
        const index = i + settings.colors[k];
        console.log(`${index} \t ${!!(imageData.data[index] & 1)}`);

        


        lengthBits.push(!!(imageData.data[index] & 1));
      }
    }


    // for (let i = 0; i < bufferLength; i++)
    //   lengthBits.push(!!(imageData.data[i] & 1));



    const symbolLength = this.dataOperationsService.bitsToNumber(lengthBits)[0];
    const bitLength = symbolLength * bufferLength;

    const extractedBits: boolean[] = [];

    for (let j = 0; j < bitLength; i += step * 4) {
      for (let k = 0; k < settings.colors.length; k++, j++) {
        const index = i + settings.colors[k];
        console.log(`${index} \t ${!!(imageData.data[index] & 1)}`);

        if (index == 509) {
          let xx = 11;
          console.log(xx);
        }

        extractedBits.push(!!(imageData.data[index] & 1));
      }
    }
    // extractedBits.push(!!(imageData.data[i] & 1));

    const numbers = this.dataOperationsService.bitsToNumber(extractedBits);
    const msg = this.dataOperationsService.numbersToString(numbers);

    return msg;
  }

  private setLSB(val: number, bit: boolean): number {
    if (bit)
      return val | 1;
    else
      return val & ~0 << 1;
  }

  // private buildImageData(base64EncodedImage: string, canvas: HTMLCanvasElement, self: StegImageInjectComponent) {
  //   var image = new Image();
  //   // var self = this;

  //   image.onload = () => {
  //     canvas.height = image.height;
  //     canvas.width = image.width;

  //     var context = canvas.getContext('2d');
  //     context.drawImage(image, 0, 0);

  //     self.imageData = context.getImageData(0, 0, image.width, image.height);

  //     // Now you can access pixel data from imageData.data.
  //     // It's a one-dimensional array of RGBA values.
  //     // Here's an example of how to get a pixel's color at (x,y)
  //     // var index = (y * imageData.width + x) * 4;
  //     // var red = imageData.data[index];
  //     // var green = imageData.data[index + 1];
  //     // var blue = imageData.data[index + 2];
  //     // var alpha = imageData.data[index + 3];
  //   };

  //   image.src = base64EncodedImage;
  // }

}
