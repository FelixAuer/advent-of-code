package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"regexp"
	"strconv"
)

func main() {
	lines := readInput()
	grid := make(map[string]int)
	for _, line := range lines {
		x1, y1, x2, y2, dirX, dirY := (parseLine(line))
		//if dirX != 0 && dirY != 0 {
		//	continue
		//}
		grid[strconv.Itoa(x1)+"-"+strconv.Itoa(y1)]++
		for !(x1 == x2 && y1 == y2) {
			x1 += dirX
			y1 += dirY
			grid[strconv.Itoa(x1)+"-"+strconv.Itoa(y1)]++
		}
	}

	overlapping := 0
	for _, val := range grid {
		if val >= 2 {
			overlapping++
		}
	}
	fmt.Println(overlapping)
}

func parseLine(line string) (int, int, int, int, int, int) {
	re, _ := regexp.Compile(`(\d+),(\d+) -> (\d+),(\d+)`)
	match := re.FindAllStringSubmatch(line, 4)
	x1, _ := strconv.Atoi(match[0][1])
	y1, _ := strconv.Atoi(match[0][2])
	x2, _ := strconv.Atoi(match[0][3])
	y2, _ := strconv.Atoi(match[0][4])
	return x1, y1, x2, y2, direction(x1, x2), direction(y1, y2)
}

func direction(start int, end int) int {
	switch {
	case start > end:
		return -1
	case end > start:
		return 1
	case start == end:
		return 0
	}
	return 0
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
