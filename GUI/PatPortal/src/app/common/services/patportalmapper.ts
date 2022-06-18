import { Injectable } from "@angular/core";
import { UserDto } from "../dtos/UserDto";
import { IUser } from "../models/IUser";

@Injectable({
    providedIn: 'root'
})
export class PatPortalMapper{

    public Create(userDto: UserDto) : IUser {
        var result =  {
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
}