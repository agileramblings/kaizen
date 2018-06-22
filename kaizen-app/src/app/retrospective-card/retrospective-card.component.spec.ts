import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RetrospectiveCardComponent } from './retrospective-card.component';

describe('RetrospectiveCardComponent', () => {
  let component: RetrospectiveCardComponent;
  let fixture: ComponentFixture<RetrospectiveCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RetrospectiveCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RetrospectiveCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
