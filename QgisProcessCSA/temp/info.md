25.1.11.28. Sample raster values			
Extracts raster values at the point locations. If the raster layer is multiband, each band is sampled.			
The attribute table of the resulting layer will have as many new columns as the raster layer band count.			
			
Parameters		
Label	名称	Type	Description
Input Layer	INPUT	[vector: point]	Point vector layer to use for sampling
Raster Layer	RASTERCOPY	[raster]	Raster layer to sample at the given point locations.
Output column prefix	COLUMN_PREFIX	[string] Default: 'SAMPLE_'	Prefix for the names of the added columns.
Output Layer	OUTPUT	[vector: point] 	Specify the output layer containing the sampled values
			
Outputs			
Label	名称	Type	Description
Sampled	OUTPUT	[vector: point]	The output layer containing the sampled values.
Python code			
Algorithm ID: native:rastersampling			
import processing			
processing.run("algorithm_id", {parameter_dictionary})			
The algorithm id is displayed when you hover over the algorithm in the Processing Toolbox. The parameter dictionary provides the parameter NAMEs and values. See Using processing algorithms from the console for details on how to run processing algorithms from the Python console.			
