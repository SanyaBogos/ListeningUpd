import { Injectable } from '@angular/core';

@Injectable()
export class DataOperationsService {

  private bufferLength: number = 16;

  constructor() { }

  public stringToBytes(str: string): number[] {
    var data: number[] = [];

    for (var i = 0; i < str.length; i++)
      data.push(str.charCodeAt(i));

    return data;
  }

  public numberToBits(num: number): boolean[] {
    var arr = [];

    for (var i = 0; i < this.bufferLength; i++)
      arr[i] = (num >> i) & 1;

    return arr;
  }

  public bitsToNumber(bits: boolean[]): number[] {
    const numbers: number[] = [];

    for (let i = 0; i < bits.length; i += this.bufferLength) {

      let val = 0;

      for (let j = 0; j < this.bufferLength; j++)
        if (bits[j + i])
          val += 1 << j;

      numbers.push(val);
    }

    return numbers;
  }

  public numbersToString(numbers: number[]): string {
    const result = String.fromCharCode(...numbers);
    return result;
  }

  public numbersToBits(nums: number[]): boolean[] {
    const bits = [];

    nums.forEach((item) => {
      bits.push(...this.numberToBits(item));
    });

    return bits;
  }

  public getBufferLength() {
    return this.bufferLength;
  }

  public getMaxLength(width: number, height: number, colorsCount: number) {
    const maxLength = Math.round(width * height * colorsCount / this.bufferLength) - 1;
    return maxLength;
  }

}
