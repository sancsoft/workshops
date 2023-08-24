import fs from 'fs/promises'

const dir = process.cwd();

console.log(`Listing files in ${dir}`);

const files = await fs.readdir(dir);

for(const file of files) {
    console.log(file);
}