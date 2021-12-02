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
	var lines []string = readInput()
	fmt.Println(partOne(lines))
	fmt.Println(partTwo(lines))
}

func partOne(lines []string) int {
	posX, posY := 0, 0

	for _, line := range lines {
		instructions := strings.Split(line, " ")
		direction := instructions[0]
		value, _ := strconv.Atoi(instructions[1])

		switch direction {
		case "forward":
			posX += value
		case "up":
			posY -= value
		case "down":
			posY += value
		}
	}

	return posX * posY
}

func partTwo(lines []string) int {
	posX, posY, aim := 0, 0, 0

	for _, line := range lines {
		instructions := strings.Split(line, " ")
		direction := instructions[0]
		value, _ := strconv.Atoi(instructions[1])

		switch direction {
		case "forward":
			posX += value
			posY += aim * value
		case "up":
			aim -= value
		case "down":
			aim += value
		}
	}

	return posX * posY
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
