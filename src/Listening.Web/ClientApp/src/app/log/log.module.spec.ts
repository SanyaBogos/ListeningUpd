import { LogModule } from './log.module';

describe('LogModule', () => {
  let LogModule: LogModule;

  beforeEach(() => {
    LogModule = new LogModule();
  });

  it('should create an instance', () => {
    expect(LogModule).toBeTruthy();
  });
});
