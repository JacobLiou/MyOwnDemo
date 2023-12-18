import cv2

print(cv2.__version__)


# Point = tuple(float, float)

class PointTest:
    def __init__(self) -> None:
        self.point = (0, 0)
    

pt = PointTest();
print(pt.point)

im = cv2.imread('../../../../../images/re.jpg')
im.show()
