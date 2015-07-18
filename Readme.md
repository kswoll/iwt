# IOs Widgeting Toolkit

There are a variety of ways to layout a UI in iOS in Xamarin.iOS.  You can use Interface Builder.  You can generate the code on the fly in conjunction with auto-layout.  But I've always been partial to code-based toolkits that are container-based.  The two main precursors that influenced me were Java Swing and Google's GWT.

The main idea is you have a container that can contain children.  (This is the case for all UI frameworks I know of)  However, distinguishingly, the container controls the layout of its children.  So you end up with a variety of containers (sometimes with a nomencleture with nouns prefixed with "Panel".  The idea is you haee containers (such as ones that render their children horizontally or vertially, based on the sequence in which they were added to the container -- or a wide vareity of other types, such as ones which arrange children in a table, along the edges (perhaps along a particular edge), with animating transitions, and really, anything else you want to try.

With these primitives, it is easy to combine them to create any sort of layout you like.  
