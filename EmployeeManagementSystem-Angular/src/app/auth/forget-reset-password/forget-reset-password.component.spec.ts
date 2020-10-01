import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgetResetPasswordComponent } from './forget-reset-password.component';

describe('ForgetResetPasswordComponent', () => {
  let component: ForgetResetPasswordComponent;
  let fixture: ComponentFixture<ForgetResetPasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForgetResetPasswordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ForgetResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
