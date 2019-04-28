const int ITERATIONS = 600;

float add(vec2 a)
{
	return a.x+a.y;
}

float sub(vec2 a)
{
	return a.x-a.y;
}

vec2 iter(vec2 z, vec2 c) {
	return vec2(sub(z*z) + c.x, 2 * z.x * z.y + c.y);
}

vec2 scaleOnTarget(vec2 uv, float k, vec2 target){
	return vec2(k*uv.x + target.x, k*uv.y + target.y);
}

float vary(){
	return .5 * sin(iTime*.2)+ .5;
}

vec3 diverges(vec2 c){
	vec2 currentZ = vec2(0.0f);
	vec2 previousZ = currentZ;
	for(int i = 0; i <= ITERATIONS; i++){
		previousZ = currentZ;
		currentZ = iter(currentZ, c);
		if (length(currentZ) > 4.0f){
			return vec3(float(i) / float(ITERATIONS));
		}
	}
	return vec3(distance(currentZ, previousZ), distance(currentZ, c), .5);
}

// main
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	// fragCoord.xy = literally pixel index away from origin. 
	vec2 uv = fragCoord.xy / iResolution.xy;
	// both x and y now have range 0-1
	vec2 uv2 = 2 * (uv - vec2(.5, .5));
	
	uv2 = scaleOnTarget(uv2, 0.005 , vec2(-0.7463, 0.1101));
	vec3 color = vec3(diverges(uv2.xy))+ .1f + iTime*vec3(.001, .001, .001);
	fragColor = vec4(color, 1.0);
}