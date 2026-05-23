import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskItemList } from './task-item-list';

describe('TaskItemList', () => {
  let component: TaskItemList;
  let fixture: ComponentFixture<TaskItemList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskItemList],
    }).compileComponents();

    fixture = TestBed.createComponent(TaskItemList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
