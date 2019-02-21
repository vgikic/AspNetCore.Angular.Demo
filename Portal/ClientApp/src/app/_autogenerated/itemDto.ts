
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IItemDto  {
    id: number;
    name: string;
    email: string;
    from: Date;
    to: Date;
    isActive: boolean;
    categoryId: number;
    optionId: number;
    parts: number[];
}

export class ItemDto  implements IItemDto{ 
    public id: number;
    public name: string;
    public email: string;
    public from: Date;
    public to: Date;
    public isActive: boolean;
    public categoryId: number;
    public optionId: number;
    public parts: number[];

    constructor(id : number = null,  name : string = null,  email : string = null,  from : Date = null,  to : Date = null,  isActive : boolean = null,  categoryId : number = null,  optionId : number = null,  parts : number[] = null ) {
        
        this.id = id;
        this.name = name;
        this.email = email;
        this.from = from;
        this.to = to;
        this.isActive = isActive;
        this.categoryId = categoryId;
        this.optionId = optionId;
        this.parts = parts;
    }
}



