import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminDaboardComponent } from './admin-daboard.component';

describe('AdminDaboardComponent', () => {
  let component: AdminDaboardComponent;
  let fixture: ComponentFixture<AdminDaboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminDaboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminDaboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
