export interface authorCreationDTO{
    name: string;
    dateOfBirth?: Date;
    picture?: File;
    pictureURL?: string;
    biography?: string;
}

export interface authorBookDTO{
    id: number;
    name: string;
    character: string;
    picture: string;
}