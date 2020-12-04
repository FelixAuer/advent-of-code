import re

INPUT_FILE = "input.txt"


class Passport:

    def __init__(self, birth_year, issue_year, expiration_year, height, hair_color, eye_color, passport_id):
        self.birth_year = birth_year
        self.issue_year = issue_year
        self.expiration_year = expiration_year
        self.height = height
        self.hair_color = hair_color
        self.eye_color = eye_color
        self.passport_id = passport_id

    def is_valid(self):
        if not re.fullmatch(r"[0-9]{4}", self.birth_year) or not (1920 <= int(self.birth_year) <= 2002):
            return False

        if not re.fullmatch(r"[0-9]{4}", self.issue_year) or not (2010 <= int(self.issue_year) <= 2020):
            return False

        if not re.fullmatch(r"[0-9]{4}", self.expiration_year) or not (2020 <= int(self.expiration_year) <= 2030):
            return False

        if not (re.fullmatch(r"1[0-9]{2}cm", self.height) or re.fullmatch(r"[0-9]{2}in", self.height)):
            return False

        if re.fullmatch(r"1[0-9]{2}cm", self.height) and not 150 <= int(self.height[0:3]) <= 193:
            return False

        if re.fullmatch(r"[0-9]{2}in", self.height) and not 59 <= int(self.height[0:2]) <= 76:
            return False

        if not re.fullmatch(r"#[0-9a-f]{6}", self.hair_color):
            return False

        if self.eye_color not in ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]:
            return False

        if not re.fullmatch(r"[0-9]{9}", self.passport_id):
            return False

        return True

    @staticmethod
    def create_passport(passport_data):
        return Passport(
            get_value(passport_data, 'byr'),
            get_value(passport_data, 'iyr'),
            get_value(passport_data, 'eyr'),
            get_value(passport_data, 'hgt'),
            get_value(passport_data, 'hcl'),
            get_value(passport_data, 'ecl'),
            get_value(passport_data, 'pid'),
        )


def main():
    unfiltered_data = load_input().split("\n\n")
    fields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"]

    valid_passport_data = [passport for passport in unfiltered_data if all(field in passport for field in fields)]
    print(len(valid_passport_data))

    passports = [Passport.create_passport(passport_data) for passport_data in valid_passport_data]
    valid_passports = list(filter(lambda passport: passport.is_valid(), passports))

    print(len(valid_passports))


def get_value(data, key):
    key_value = [pair for pair in data.split() if key in pair][0]
    return key_value.split(':')[1]


def load_input():
    return open(INPUT_FILE, "r").read().strip()


if __name__ == '__main__':
    main()
