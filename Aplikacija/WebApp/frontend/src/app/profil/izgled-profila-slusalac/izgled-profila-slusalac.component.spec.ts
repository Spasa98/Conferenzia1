import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzgledProfilaSlusalacComponent } from './izgled-profila-slusalac.component';

describe('IzgledProfilaSlusalacComponent', () => {
  let component: IzgledProfilaSlusalacComponent;
  let fixture: ComponentFixture<IzgledProfilaSlusalacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzgledProfilaSlusalacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzgledProfilaSlusalacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
