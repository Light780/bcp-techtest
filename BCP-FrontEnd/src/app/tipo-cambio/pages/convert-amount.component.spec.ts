import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConvertAmountComponent } from './convert-amount.component';

describe('ConvertAmountComponent', () => {
  let component: ConvertAmountComponent;
  let fixture: ComponentFixture<ConvertAmountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConvertAmountComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConvertAmountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
