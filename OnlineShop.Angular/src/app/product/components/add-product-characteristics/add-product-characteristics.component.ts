import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProductCharacteristics } from 'app/product/models';

@Component({
  selector: 'app-add-product-characteristics',
  templateUrl: './add-product-characteristics.component.html',
  styleUrls: ['./add-product-characteristics.component.scss']
})
export class AddProductCharacteristicsComponent implements OnInit {

  form: FormGroup;

  constructor(public dialogRef: MatDialogRef<AddProductCharacteristicsComponent>,
    @Inject(MAT_DIALOG_DATA) public characteristics: ProductCharacteristics) { }

  ngOnInit() {
    this.form = new FormGroup({
      key: new FormControl('', Validators.required),
      value: new FormControl('', Validators.required)
    });
  }

  onClickSave(): void {
    if (!this.form.valid) {
      return;
    }

    this.dialogRef.close(this.form.getRawValue());
  }

  onClickCancel(): void {
    this.dialogRef.close();
  }

}
