import { IPost } from "src/app/common/models/IPost";

export interface IExtendedPost{
    post: IPost,
    showComments: boolean
}