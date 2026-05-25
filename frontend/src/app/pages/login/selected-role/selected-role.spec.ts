import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedRole } from './selected-role';

describe('SelectedRole', () => {
  let component: SelectedRole;
  let fixture: ComponentFixture<SelectedRole>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectedRole],
    }).compileComponents();

    fixture = TestBed.createComponent(SelectedRole);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
