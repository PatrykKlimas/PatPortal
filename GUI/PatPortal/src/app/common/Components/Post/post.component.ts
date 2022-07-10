import { Component, EventEmitter, Input, Output } from "@angular/core";
import { IPost } from "../../models/IPost";

@Component({
    selector: "pp-post",
    templateUrl: "./post.component.html",
    styleUrls: ["./post.component.css"]
})
export class PostCommponent{
    @Input() post: IPost | undefined;
    @Output() toogleCommentsReaction = new EventEmitter();
    @Output() postCommentReaction = new EventEmitter<{post: IPost, content: string}>();
    @Input() showComments: boolean = false;

    toogleComments(){
        if(this.post)
            this.toogleCommentsReaction.emit();
    }

    postComment(content: string){
        if(this.post)
            this.postCommentReaction.emit({post: this.post, content: content});
    }
}
