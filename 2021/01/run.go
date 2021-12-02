package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
)

func main() {
	var lines []int = readInput()
	fmt.Println(countIncreases(1, lines))
	fmt.Println(countIncreases(3, lines))
}

func countIncreases(windowWidth int, lines []int) int {
	increases := 0
	current := windowSum(0, windowWidth, lines)

	for i := 1; i <= (len(lines) - windowWidth); i++ {
		next := windowSum(i, windowWidth, lines)

		if next > current {
			increases++
		}

		current = next
	}

	return increases
}

func windowSum(position int, length int, lines []int) int {
	return addArray(lines[position : position+length])
}

func addArray(numbs []int) int {
	result := 0
	for _, numb := range numbs {
		result += numb
	}
	return result
}

func readInput() []int {
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
	var lines []int

	for scanner.Scan() {
		next, _ := strconv.Atoi(scanner.Text())
		lines = append(lines, next)
	}

	// The method os.File.Close() is called
	// on the os.File object to close the file
	file.Close()

	return lines
}
