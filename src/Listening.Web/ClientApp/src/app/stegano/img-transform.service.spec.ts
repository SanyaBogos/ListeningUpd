import { TestBed } from '@angular/core/testing';

import { ImgTransformService } from './img-transform.service';

describe('ImgTransformService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ImgTransformService = TestBed.get(ImgTransformService);
    expect(service).toBeTruthy();
  });
});
