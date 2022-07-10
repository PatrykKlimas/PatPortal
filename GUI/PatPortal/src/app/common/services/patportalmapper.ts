import { Injectable } from "@angular/core";
import { CommentDto } from "../dtos/CommentDto";
import { PostDto } from "../dtos/PostDto";
import { UserDto } from "../dtos/UserDto";
import { IComment } from "../models/IComment";
import { IPost } from "../models/IPost";
import { IUser } from "../models/IUser";

@Injectable({
    providedIn: 'root'
})
export class PatPortalMapper {

    public CreateUser(userDto: UserDto): IUser {
        var result = {
            Id: userDto.id,
            FirstName: userDto.firstName,
            LastName: userDto.lastName,
            Email: userDto.email,
            Profession: userDto.profession,
            DayOfBirht: userDto.dayOfBirht,
            Photo: userDto.photo
        };

        return result;
    }

    public CreatePost(postDto: PostDto): IPost {
        return {
            Id: postDto.id,
            Content: postDto.content,
            OwnerId: postDto.ownerId,
            OwnerName: postDto.ownerName,
            AddedDate: postDto.addedDate,
            EditedTime: postDto.editedTime,
            Photo: postDto.photo,
            AreCommentsLoaded: false,
            Comments: []
        };
    }

    public CreateComment(commentDto: CommentDto) : IComment{
        return{
            Id: commentDto.id,
            OwnerId: commentDto.ownerId,
            OwnerName: commentDto.ownerName,
            Content: commentDto.content,
            AddedDate: commentDto.addedDate,
            EditedTime: commentDto.editedTime,
            PostId: commentDto.postId
        }
    }
}