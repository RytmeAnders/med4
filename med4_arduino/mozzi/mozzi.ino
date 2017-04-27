#include <MozziGuts.h>
#include <Oscil.h>
#include <tables/sin2048_int8.h>

#define CONTROL_RATE 128

Oscil <2048, AUDIO_RATE> aSin(SIN2048_DATA);

void setup() {
  startMozzi(CONTROL_RATE);
  aSin.setFreq(440);

}

void updateControl(){
  // put changing controls in here
}


int updateAudio(){
  return aSin.next(); // return an int signal centred around 0
}


void loop(){
  audioHook(); // required here
}
