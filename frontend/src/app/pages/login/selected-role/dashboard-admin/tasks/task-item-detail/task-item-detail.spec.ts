import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskItemDetail } from './task-item-detail';

describe('TaskItemDetail', () => {
  let component: TaskItemDetail;
  let fixture: ComponentFixture<TaskItemDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskItemDetail],
    }).compileComponents();

    fixture = TestBed.createComponent(TaskItemDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
