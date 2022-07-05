import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: 'pp-inputBox',
    templateUrl: './inputBox.component.html',
    styleUrls: ['./inputBox.component.css']
})
export class InputBoxComponent{
    @Input() title: string = "";
    @Output() textValue = new EventEmitter<string>();

    text : string = '';

    postClick(text : string) : void{
        if(text.length > 0){
            this.textValue.emit(text);
        }
    }
}