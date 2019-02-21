import { BehaviorSubject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FileDto } from '../../_autogenerated/fileDto';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GalleryService } from '../../services/gallery.service';
import { NavigationService } from '../../services/navgation.service';
import { FileBindingModel } from '../../_autogenerated/fileBindingModel';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  public form: FormGroup;
  public files$: BehaviorSubject<FileDto[]>;
  public isUploading = false;
  public galleryDataSource = [];
  constructor(
    private formBuilder: FormBuilder,
    private galleryService: GalleryService,
    private navigationService: NavigationService
  ) { }

  ngOnInit(): void {
    this.loadFiles(files => {
      if (files) {
        this.setGalleryDataSource(files);
        this.files$ = new BehaviorSubject(files);
      }
    });
    this.form = this.formBuilder.group({
      files: [[]]
    });
  }

  private setGalleryDataSource = (files: FileDto[]) =>
    this.galleryDataSource = files.map(file => this.getSrc(file.base64));

  public back = () =>
    this.navigationService.GoToItemOVerview();

  public loadFiles = (callback: (files: FileDto[]) => void) =>
    this.galleryService.getUploadedFiles().subscribe(callback);

  public upload() {
    this.isUploading = true;

    const files: File[] = this.form.get('files').value;
    const model: FileBindingModel[] = [];

    for (let i = 0; i < files.length; ++i) {
      const reader = new FileReader();
      reader.readAsArrayBuffer(files[i]);

      reader.onload = () => {
        const arrayBuffer: ArrayBuffer = (reader.result as ArrayBuffer);
        const bytes = this.arrayBufferToArray(arrayBuffer);
        model.push(new FileBindingModel(bytes, files[i].name));

        if (model.length === files.length) {
          this.galleryService.uploadFiles(model).subscribe(r => {
            var sub = this.files$.subscribe(response => this.loadFiles(files => {
              this.setGalleryDataSource(files);
              this.files$.next(files);
              this.form.get('files').reset([]);
              sub.unsubscribe();
              this.isUploading = false;
            }));
          });
        }
      }
    }
  }

  public getHref = (fileName: string) =>
    this.galleryService.getFileUrl(fileName);

  public deleteFile = (fileName: string) => {
    this.galleryService.deleteFile(fileName).subscribe(response => {
      this.loadFiles(files => {
        this.setGalleryDataSource(files);
        this.files$.next(files);
      })
    })
  }

  public getSrc = (base64: string) => `data:image/png;base64,${base64}`;

  public isUploadBtnDisabled = () => !this.form.get('files').value.length || this.isUploading;

  public getTooltipTxt = (file) => `${file.size} (bytes) - ${file.type}`;

  private arrayBufferToArray(buffer): number[] {
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    const result: number[] = [];

    for (let i = 0; i < len; i++) {
      result[i] = bytes[i];
    }

    return result;
  }
}