package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	line := readInput()[0]
	var fishes [9]int
	for i := range fishes {
		fishes[i] = strings.Count(line, strconv.Itoa(i))
	}
	fmt.Println(fishes)
	for i := 0; i < 256; i++ {
		var nextGen [9]int
		for j := 0; j <= 7; j++ {
			nextGen[j] = fishes[j+1]
		}
		nextGen[8] = fishes[0]
		nextGen[6] += fishes[0]

		for j := range fishes {
			fishes[j] = nextGen[j]
		}
	}
	sum := 0
	for j := range fishes {
		sum += fishes[j]
	}
	fmt.Println(sum)
}

func readInput() []string {
	file, err := os.Open("input.txt")

	if err != nil {
		log.Fatalf("failed to open")

	}

	// The bufio.NewScanner() function is called in which the
	// object os.File passed as its parameter and this returns a
	// object bufio.Scanner which is further used on the
	// bufio.Scanner.Split() method.
	scanner := bufio.NewScanner(file)

	// The bufio.ScanLines is used as an
	// input to the method bufio.Scanner.Split()
	// and then the scanning forwards to each
	// new line using the bufio.Scanner.Scan()
	// method.
	scanner.Split(bufio.ScanLines)
	var lines []string

	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	// The method os.File.Close() is called
	// on the os.File object to close the file
	file.Close()

	return lines
}
