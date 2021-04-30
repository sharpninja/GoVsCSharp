package main

import (
	"io/ioutil"
	"log"
	_ "os"
)
const _filename string = "../../test-data/unencrypted/test.txt"
func main() {
    // Read file to byte slice
	_, er := loadFile(_filename)
	if er != nil {
		log.Fatal(er)
	}
}

func loadFile(filename string) ([]byte, error) {
	bytes, err := ioutil.ReadFile(filename)
	if err != nil {
		log.Fatal(err)
	}
	//log.Printf("Length: %d, First: %d, Last: %d", len(bytes), bytes[0], bytes[len(bytes)-1])
	return bytes, err
}
