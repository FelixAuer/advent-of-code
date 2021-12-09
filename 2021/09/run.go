package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	lines := readInput()
	fmt.Println(partOne(lines))

}

func partOne(lines []string) int {
	heightmap := make([][]string, len(lines)+2)
	for i := range heightmap {
		heightmap[i] = make([]string, len(strings.Split(lines[0], ""))+2)
	}
	length := len(heightmap)
	width := len(heightmap[0])
	for i := 0; i < len(heightmap); i++ {
		var line string
		if i != 0 && i != length-1 {
			line = lines[i-1]
		}
		for j := 0; j < width; j++ {
			if i == 0 || i == length-1 || j == 0 || j == width-1 {
				heightmap[i][j] = "9"
			} else {
				heightmap[i][j] = line[j-1 : j]
			}
		}
	}

	sumRisk := 0
	var lowPoints [][2]int
	for i := 1; i < len(heightmap)-1; i++ {

		for j := 1; j < width-1; j++ {
			val := heightmap[i][j]
			top := heightmap[i-1][j]
			bottom := heightmap[i+1][j]
			left := heightmap[i][j-1]
			right := heightmap[i][j+1]
			if val < top && val < bottom && val < left && val < right {
				v, _ := strconv.Atoi(val)
				sumRisk += v + 1
				lowPoints = append(lowPoints, [2]int{i, j})
			}
		}
	}
	fmt.Println(sumRisk)

	var basinSizes []int
	for _, coords := range lowPoints {
		var stack [][2]int
		stack = push(stack, coords)
		count := 0

		for len(stack) > 0 {
			var toCheck [2]int
			toCheck, stack = pop(stack)
			i, j := toCheck[0], toCheck[1]
			if heightmap[i][j] != "X" {
				heightmap[i][j] = "X"

				count++
			}

			if heightmap[i-1][j] != "X" && heightmap[i-1][j] != "9" {
				stack = push(stack, [2]int{i - 1, j})
			}
			if heightmap[i+1][j] != "X" && heightmap[i+1][j] != "9" {
				stack = push(stack, [2]int{i + 1, j})
			}
			if heightmap[i][j-1] != "X" && heightmap[i][j-1] != "9" {
				stack = push(stack, [2]int{i, j - 1})
			}
			if heightmap[i][j+1] != "X" && heightmap[i][j+1] != "9" {
				stack = push(stack, [2]int{i, j + 1})
			}
		}
		basinSizes = append(basinSizes, count)
	}
	printHeightmap(heightmap)
	sort.Ints(basinSizes)
	prod := 1
	for _, val := range basinSizes[len(basinSizes)-3:] {
		prod *= val
	}
	fmt.Println(prod)
	return 1
}

func printHeightmap(heightmap [][]string) {
	for _, line := range heightmap {
		fmt.Println(line)
	}
}

func push(stack [][2]int, element [2]int) [][2]int {
	return append(stack, element)
}

func pop(stack [][2]int) ([2]int, [][2]int) {
	n := len(stack) - 1
	if n == 0 {
		var v [][2]int
		return stack[0], v
	}
	return stack[n], stack[:n]
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
