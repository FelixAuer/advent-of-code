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
	fmt.Println(partTwo(lines))
}

func partOne(lines []string) int {
	count := 0
	for _, line := range lines {
		count += count1478(line)
	}
	return count
}

func partTwo(lines []string) int {
	sum := 0
	for _, line := range lines {
		words := strings.Split(line, " | ")[0]
		words = strings.TrimSpace(words)
		wordsArray := strings.Split(words, " ")
		segMap := getSegMap(wordsArray)

		words = strings.Split(line, " | ")[1]
		words = strings.TrimSpace(words)
		wordsArray = strings.Split(words, " ")
		numbString := ""
		for _, word := range wordsArray {
			numbString += toNumber(word, segMap)
		}
		val, _ := strconv.Atoi(numbString)
		sum += val
	}
	return sum
}

func reverseMap(m map[string]string) map[string]string {
	n := make(map[string]string, len(m))
	for k, v := range m {
		n[v] = k
	}
	return n
}

func toNumber(scrambled string, segMap map[string]string) string {
	numMap := map[string]string{
		"abcefg":  "0",
		"cf":      "1",
		"acdeg":   "2",
		"acdfg":   "3",
		"bcdf":    "4",
		"abdfg":   "5",
		"abdefg":  "6",
		"acf":     "7",
		"abcdefg": "8",
		"abcdfg":  "9",
	}

	unscrambled := ""
	for i := 0; i < len(scrambled); i++ {
		unscrambled += segMap[scrambled[i:i+1]]
	}
	unscrambled = sortString(unscrambled)
	return numMap[unscrambled]
}

func sortString(w string) string {
	s := strings.Split(w, "")
	sort.Strings(s)
	return strings.Join(s, "")
}

func getSegMap(input []string) map[string]string {
	segments := []string{"a", "b", "c", "d", "e", "f", "g"}
	segCount := make(map[string]int)
	segMap := make(map[string]string)
	var one, four, seven, eight string

	for _, word := range input {
		length := len(word)
		switch length {
		case 2:
			one = word
		case 4:
			four = word
		case 3:
			seven = word
		case 7:
			eight = word
		}
		for _, seg := range segments {
			segCount[seg] += strings.Count(word, seg)
		}
	}
	segMap["a"] = strings.ReplaceAll(strings.ReplaceAll(seven, one[0:1], ""), one[1:2], "")
	for seg, count := range segCount {
		switch count {
		case 6:
			segMap["b"] = seg
		case 4:
			segMap["e"] = seg
		case 9:
			segMap["f"] = seg
		}
	}
	segMap["c"] = strings.ReplaceAll(one, segMap["f"], "")
	segMap["d"] = strings.ReplaceAll(strings.ReplaceAll(strings.ReplaceAll(four, one[0:1], ""), one[1:2], ""), segMap["b"], "")
	g := eight
	for _, seg := range segMap {
		g = strings.ReplaceAll(g, seg, "")
	}
	segMap["g"] = g
	return reverseMap(segMap)
}

func count1478(line string) int {
	words := strings.Split(line, " | ")[1]
	words = strings.TrimSpace(words)
	wordsArray := strings.Split(words, " ")

	count := 0
	for _, word := range wordsArray {
		if length := len(word); length == 2 || length == 4 || length == 3 || length == 7 {
			count++
		}
	}
	return count
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
