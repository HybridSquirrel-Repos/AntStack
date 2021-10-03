class Ant:
    _antName = ""
    _antLegs = 0

    def __init__(self, name, legs):
        self._antLegs = legs
        self._antName = name

    def get_name(self):
        return self._antName

    def get_legs(self):
        return self._antLegs

