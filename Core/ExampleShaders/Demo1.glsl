/*
 * "Seascape" by Alexander Alekseev aka TDM - 2014
 * License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
 * Contact: tdmaav@gmail.com
 */
 
const int NUM_STEPS = 20;
const float PI	 	= 3.141592;
const float EPSILON	= 1e-3;
#define EPSILON_NRM (0.1 / iResolution.x)

// sea
const int ITER_GEOMETRY = 5;//3;
const int ITER_FRAGMENT = 5;//5;
const float SEA_HEIGHT = 0.4;
const float SEA_CHOPPY =5.0;
const float SEA_SPEED = 0.9;
const float SEA_FREQ = 1.05;

const float FREQUENCY_GROWTH = 1.0;
const float AMPLITUDE_GROWTH = 0.4;


const vec3 SEA_WATER_COLOR = vec3(0.25,.65,.65);
const vec3 SKY_COLOR = vec3(.4,0.01,0.1);

#define SEA_TIME (1.0 + iTime * SEA_SPEED)
const mat2 octave_m = mat2(3.0,2.0,-1.0,1.0);




// math
// This is translating 3 vectors into a matrix
// Probably not super interesting to dive into
mat3 fromEuler(vec3 ang) {
	vec2 a1 = vec2(sin(ang.x),cos(ang.x));
    vec2 a2 = vec2(sin(ang.y),cos(ang.y));
    vec2 a3 = vec2(sin(ang.z),cos(ang.z));
    mat3 m;
    m[0] = vec3(a1.y*a3.y+a1.x*a2.x*a3.x,a1.y*a2.x*a3.x+a3.y*a1.x,-a2.y*a3.x);
	m[1] = vec3(-a2.y*a1.x,a1.y*a2.y,a2.x);
	m[2] = vec3(a3.y*a1.x*a2.x+a1.y*a3.x,a1.x*a3.x-a1.y*a3.y*a2.x,a2.y*a3.y);
	return m;
}
// This only feeds into 'noise' below
float hash( vec2 p ) {
	float h = dot(p,vec2(127.1,311.7));	
    return fract(sin(h)*43758.5453123);
}
float noise( in vec2 p ) {
    vec2 i = floor( p );
    vec2 f = fract( p );	
	vec2 u = f*f*(3.0-2.0*f);
    return -1.0+2.0*mix( mix( hash( i + vec2(0.0,0.0) ), 
                     hash( i + vec2(1.0,0.0) ), u.x),
                mix( hash( i + vec2(0.0,1.0) ), 
                     hash( i + vec2(1.0,1.0) ), u.x), u.y);
}

// lighting
// n = a vector that ...

// sea
float sea_octave(vec2 uv, float choppy) {
    //uv += noise(uv);        
    vec2 wv = 1.0-abs(sin(uv));
    vec2 swv = abs(cos(uv));    
    wv = mix(wv,swv,wv);
    return pow(1.0-pow(wv.x * wv.y,0.65),choppy);
}

float map(vec3 p, int iter) {
    float freq = SEA_FREQ;
    float amp = SEA_HEIGHT;
    float choppy = SEA_CHOPPY;
    vec2 uv = p.xz;
    
    float d, h = 0.0;    
    for(int i = 0; i < iter; i++) {        
    	d =+ sea_octave((uv+SEA_TIME)*freq,choppy);
        h += d * amp;        
    	uv *= octave_m;
		freq *= FREQUENCY_GROWTH;
		amp *= AMPLITUDE_GROWTH;
        choppy = mix(choppy,1.0,0.2);
    }
    return p.y - h;
}

vec3 getSeaColor(vec3 normal, vec3 eye) {  
    float fresnel = clamp(1.0 - dot(normal,-eye), 0.0, 1.0);
    fresnel = pow(fresnel,3.0) * 0.75;
	  
    vec3 reflected = SKY_COLOR;
    vec3 refracted = SEA_WATER_COLOR; 

    vec3 color = mix(refracted,reflected,fresnel);
    
    return color;
}

// tracing
vec3 getNormal(vec3 p, float eps) {
    vec3 n;
    n.y = map(p, ITER_FRAGMENT);    
    n.x = map(vec3(p.x+eps,p.y,p.z), ITER_FRAGMENT) - n.y;
    n.z = map(vec3(p.x,p.y,p.z+eps), ITER_FRAGMENT) - n.y;
    n.y = eps;
    return normalize(n);
}

float heightMapTracing(vec3 ori, vec3 dir, out vec3 p) {  
    float tm = 0.0;
    float tx = 1000.0;    
    float hx = map(ori + dir * tx, ITER_GEOMETRY);
    if(hx > 0.0) return tx;   
    float hm = map(ori + dir * tm, ITER_GEOMETRY);    
    float tmid = 0.0;
    for(int i = 0; i < NUM_STEPS; i++) {
        tmid = mix(tm,tx, hm/(hm-hx));                   
        p = ori + dir * tmid;                   
    	float hmid = map(p, ITER_GEOMETRY);
		if(hmid < 0.0) {
        	tx = tmid;
            hx = hmid;
        } else {
            tm = tmid;
            hm = hmid;
        }
    }
    return tmid;
}



// main
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 uv = fragCoord.xy / iResolution.xy;
    uv = uv * 2.0 - 1.0;
    uv.x *= iResolution.x / iResolution.y;    
    float time = 0.0f;
        
    // ray
	
	float cameraHeight = 2.5f;
	
    vec3 cameraAngle = vec3(0.0,0.45,0.0);    
    vec3 cameraOrigin = vec3(0.0,cameraHeight,0.0);
	
	float depthFactor = -1.5f;
    vec3 cameraDirection = normalize(vec3(uv.xy,depthFactor)) * fromEuler(cameraAngle);

    // tracing
    vec3 p;
    heightMapTracing(cameraOrigin,cameraDirection,p);
    vec3 dist = p - cameraOrigin;
    vec3 normal = getNormal(p, dot(dist,dist) * EPSILON_NRM);
    vec3 light = normalize(vec3(0.0,0.40,0.8)); 
             
    // color
    vec3 color = mix(
        SKY_COLOR,
        getSeaColor(normal,cameraDirection),
    	smoothstep(0.0,-.2,cameraDirection.y));
        
    // post
	//	fragColor = vec4(pow(color,vec3(0.75)), 1.0);

	fragColor = vec4(color, 1.0);
}