import { IComment } from "./IComment";

export interface IPost{
    Id: string;
    Content : string;
    OwnerId : string;
    OwnerName: string;
    AddedDate : string;
    EditedTime :string;
    Photo : any;
    AreCommentsLoaded: boolean,
    Comments: IComment[]
}