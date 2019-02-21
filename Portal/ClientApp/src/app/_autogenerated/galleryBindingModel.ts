import {FileBindingModel} from './fileBindingModel';

//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IGalleryBindingModel  {
    files: FileBindingModel[];
}

export class GalleryBindingModel  implements IGalleryBindingModel{ 
    public files: FileBindingModel[];

    constructor(files : FileBindingModel[] = null ) {
        
        this.files = files;
    }
}



