import { Component, Input } from '@angular/core';

@Component({
  selector: 'pm-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent{
  @Input() title : string = '';
  constructor() { }



}
