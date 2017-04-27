#include <SoftwareSerial.h>
#include <SerialCommand.h>
#include "GY_85.h"
#include <Wire.h>

SerialCommand sCmd;
GY_85 GY85; // Create object of GY_85

void setup() {
    Wire.begin();
    delay(10);
    Serial.begin(9600);
    
    delay(10);
    GY85.init();
    delay(10);
    while(!Serial);
    sCmd.addCommand("PING", pingHandler);
    sCmd.addCommand("ECHO", echoHandler);
}

void loop() {
  if (Serial.available() > 0){
      sCmd.readSerial();
  }

    int ax = GY85.accelerometer_x( GY85.readFromAccelerometer() );
    int ay = GY85.accelerometer_y( GY85.readFromAccelerometer() );
    int az = GY85.accelerometer_z( GY85.readFromAccelerometer() );
    
    int cx = GY85.compass_x( GY85.readFromCompass() );
    int cy = GY85.compass_y( GY85.readFromCompass() );
    int cz = GY85.compass_z( GY85.readFromCompass() );

    float gx = GY85.gyro_x( GY85.readGyro() );
    float gy = GY85.gyro_y( GY85.readGyro() );
    float gz = GY85.gyro_z( GY85.readGyro() );
    float gt = GY85.temp  ( GY85.readGyro() );
    
    Serial.print  ( "accelerometer" );
    Serial.print  ( " x:" );
    Serial.print  ( ax );
    Serial.print  ( " y:" );
    Serial.print  ( ay );
    Serial.print  ( " z:" );
    Serial.print  ( az );
    
    Serial.print  ( "  compass" );
    Serial.print  ( " x:" );
    Serial.print  ( cx );
    Serial.print  ( " y:" );
    Serial.print  ( cy );
    Serial.print  (" z:");
    Serial.print  ( cz );
    
    Serial.print  ( "  gyro" );
    Serial.print  ( " x:" );
    Serial.print  ( gx );
    Serial.print  ( " y:" );
    Serial.print  ( gy );
    Serial.print  ( " z:" );
    Serial.print  ( gz );
    Serial.print  ( " gyro temp:" );
    Serial.println( gt );

    Serial.write(ax);
    //Serial.flush();
    
    delay(50);             // only read every 0,5 seconds, 10ms for 100Hz, 20ms for 50Hz
}

void pingHandler() {
  Serial.println("PONG");
}

void echoHandler () {
  char *arg;
  arg = sCmd.next();
  if (arg != NULL)
    Serial.println(arg);
  else
    Serial.println("nothing to echo");
}

