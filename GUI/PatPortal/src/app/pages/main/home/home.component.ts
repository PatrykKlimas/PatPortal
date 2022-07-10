import { Component } from "@angular/core";
import { Store } from "@ngrx/store";
import { map, tap } from "rxjs";
import { IPost } from "src/app/common/models/IPost";
import { IUser } from "src/app/common/models/IUser";
import * as GlobalSelectors from "../../../redux/globas.selectors";
import * as MainActions from "../redux/main.actions";
import { mainReducer, MainState } from "../redux/main.reducers";
import * as MainSelectors from "../redux/main.selectors";
import { IExtendedPost } from "./IExntededPost";

@Component({
    selector: 'pp-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {

    constructor(private store: Store<MainState>) { }

    extendedPosts: IExtendedPost[] = [];
    user: IUser | null = null;

    $posts = this.store.select(MainSelectors.getPosts)
        .pipe(
            tap(posts => {
                posts.forEach(post => {
                    var extednedPost = this.extendedPosts.find(ePost => ePost.post.Id == post.Id);
                    this.extendedPosts = this.extendedPosts.filter(ePost => ePost.post.Id != post.Id);
                    var showComment: boolean = false;
                    if (extednedPost)
                        showComment = extednedPost.showComments;
                    this.extendedPosts.push({ post: post, showComments: showComment });
                });
            }),
            map(posts => posts)
        ).subscribe();

    $currentUser = this.store.select(GlobalSelectors.getUser)
        .pipe(
            tap(user => {
                this.user = user;
                if (user !== null)
                    this.store.dispatch(MainActions.initializePosts({ userId: user.Id }));
            })
        );

    toogleComments(post: IPost): void {
        var extendedPost = this.extendedPosts.find(ePost => ePost.post.Id === post.Id);

        //ToDo veryfy why after add new post before show comments we do not see other comments
        if (extendedPost) {

            var newExtendedPost: IExtendedPost = {
                post: extendedPost?.post,
                showComments: !extendedPost?.showComments
            };

            this.extendedPosts = this.extendedPosts.map(ePost => ePost.post.Id != post.Id ? ePost : newExtendedPost);

            if (newExtendedPost.post.AreCommentsLoaded === false && newExtendedPost.showComments)
                this.store.dispatch(MainActions.initializeComments({ postId: post.Id }));

        }
    }

    addComment(post: IPost, content: string){
        if(this.user != null)
            this.store.dispatch(MainActions.initializePostComment({postId: post.Id, content: content, ownerId: this.user?.Id} ))
    }
}