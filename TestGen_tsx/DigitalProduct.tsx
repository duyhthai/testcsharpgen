
class DigitalProduct extends Product {
  private fileSize: number;

  constructor(name: string, fileSize: number) {
    super(name);
    this.fileSize = fileSize;
  }

  getInfo(): string {
    return `${super.getInfo()} (File size: ${this.fileSize}MB)`;
  }
}
