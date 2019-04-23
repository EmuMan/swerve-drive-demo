# swerve-drive-demo
A demo of a simple swerve drive system.

## How it works
This method is based off of two types of motion: rotational motion and linear movement.

If you see anything strange in trig functions and stuff, it's probably because most of the calculations are done CW from the positive y-axis for convenience reasons (or something), but typical trig functions go CCW from the positive x-axis. Just something to keep in mind.

### Classes
 * `Wheel` - Handles all of the controlling of the wheels, including the binding between the physical models and the `WheelCollider`s. Also allows a rotation speed to be set, more closely emulating an actual robot.
 * `SwerveDrive` - Controls each of the wheels. Detailed below.

### Calculating rotational motion vectors
The rotational motion vectors are fairly simple, essentially just forming a circle with the wheels. Because there are only four wheels arranged in a perfect square, the vectors are as simple as the normalized versions of stuff such as (1, 1) and (1, -1) multiplied by how far the right stick is pushed along the x-axis. This solves two problems at once: magnitude and the inverse direction. I will likely be adding support for any configuration of any number of wheels soon, which will make it a little more complicated but still not anything crazy.

### Calculating linear movement vectors
Linear movement vectors are also fairly simple. We can just use the x and y values of the left stick as a vector and then apply the robot's rotation to that by converting it to degrees + magnitude, doing some basic math, and converting it back.

### Adding the vectors
There's really nothing special about this step. Just add the vectors, convert to degrees + magnitude, and apply those values to the wheels.
