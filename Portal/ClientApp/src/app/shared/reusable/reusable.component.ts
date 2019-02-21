import { DataService } from '../../services/data.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-reusable',
  templateUrl: './reusable.component.html',
  styleUrls: ['./reusable.component.scss']
})
export class ReusableComponent implements OnInit {

  public data: string[];
  @Input() exampleType: ExampleType;
  @Output() notifyParent = new EventEmitter<string>();
  @Input() dataFromParent: { id: number, email: string, address: string };

  constructor(private dataService: DataService) {}

  ngOnInit() {
    this.data = this.dataService.data;
  }

  public onBtnClick = () => {
    this.notifyParent.emit('VERY GOOD JOB!');
  }

  public alertUser = () => {
    alert("We'v got a 'professional' button clicker over here!");
  }

}
enum ExampleType {
  Basic,
  DataFromService,
  ParentChildCommunication,
  ChildParentCommunication,
  CallingChildFunctions,
  ComponentContentProjection
}
