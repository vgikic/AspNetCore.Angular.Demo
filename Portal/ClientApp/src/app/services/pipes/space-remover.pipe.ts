import { PipeTransform, Pipe } from "@angular/core";

@Pipe({
    name:'spaceRemover'
})
export class SpaceRemoverPipe implements PipeTransform{
    transform(value:string) {
       return value.split(' ').join('');
    }
}