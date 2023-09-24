import {ComponentFixture, TestBed} from '@angular/core/testing';

import {TweetDisplayComponent} from './tweet-display.component';

describe('TweetDisplayComponent', () => {
  let component: TweetDisplayComponent;
  let fixture: ComponentFixture<TweetDisplayComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TweetDisplayComponent]
    });
    fixture = TestBed.createComponent(TweetDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
