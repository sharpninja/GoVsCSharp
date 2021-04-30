package main

import (
	"log"
	"os"
	"testing"
)

func TestMain(m *testing.M) {
	os.Exit(m.Run())
}

func BenchmarkLoadFile(b *testing.B) {
    for i := 0; i < b.N; i++ {
		_, err := loadFile(_filename)
		if err != nil {
			log.Fatal(err)
		}
    }
}
