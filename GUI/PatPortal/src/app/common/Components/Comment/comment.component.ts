import { Component, Input } from "@angular/core";
import { IComment } from "../../models/IComment";
import { IUser } from "../../models/IUser";

@Component({
    selector: "pp-comment",
    templateUrl: "./comment.component.html",
    styleUrls: ["./comment.component.css"]
})
export class CommentComponent{
    @Input() comment: IComment | undefined;
}